import { Component, Input } from '@angular/core';
import { BlockDto } from '../../models/BlockDto';
import { RequestService } from '../../services/request.service';
import { ArticleDto } from '../../models/ArticleDto';
import { CreateRequestDto } from '../../models/CreateRequestDto';
import { RecipeDto } from '../../models/RecipeDto';
import { TrainingDto } from '../../models/TrainingDto';
import { RequestDto } from '../../models/Request';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ChangeRequestDto } from '../../models/ChangeRequestDto';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrl: './request.component.scss'
})
export class RequestComponent {
  requestBlocks: BlockDto[] = [];
  isArticle: boolean = false; 
  isRecipe: boolean = false; 
  isTraining: boolean = false; 
  isEmpty: boolean = true;
  isAdmin: boolean = false;


  constructor(private requestService: RequestService, private route:ActivatedRoute, private userService: AuthService) {
   
   this.route.queryParams.subscribe(queryParam => {
      if(queryParam['id']) { 
        this.isEmpty = false;
        this.isAdmin = this.userService.getRoles().lastIndexOf("Admin") == -1 ? false : true; 
        this.getRequest(queryParam['id'])
      }
      else {
        this.addNewBlock();
      }
   });
  }

  getRequest(id: number) {
    this.requestService.getRequest(id).subscribe(x => {
      console.log(x);
      if(x.article) {
        this.isArticle = true;
        this.articleDto.name = x.article.name;
        this.articleDto.id = x.article.id;
        this.articleDto.creatorId = x.article.creatorId;
        this.articleDto.statusTypeId = x.article.statusTypeId;
        this.requestBlocks = x.article.blocks;
        console.log(this.articleDto);
      }

      if(x.recipe) {
        this.isRecipe = true;
        this.recipeDto.name = x.recipe.name;
        this.recipeDto.id = x.recipe.id;
        this.recipeDto.creatorId = x.recipe.creatorId;
        this.recipeDto.statusTypeId = x.recipe.statusTypeId;
        this.requestBlocks = x.recipe.blocks!;
        this.recipeDto.caloriesCount = x.recipe.caloriesCount;
        this.recipeDto.fatsCount = x.recipe.fatsCount;
        this.recipeDto.carbsCount = x.recipe.carbsCount;
        this.recipeDto.proteinsCount = x.recipe.proteinsCount;
      }

      if(x.training) {
        this.isTraining = true;
        this.trainingDto = x.training;
      }
    });
  }

  onChange(target: any) {

    this.isArticle = false;
    this.isRecipe = false;
    this.isTraining = false;
    let value = target.target.value;
    if(value == 1) this.isArticle = true;
    if(value == 2) this.isRecipe = true;
    if(value == 3) this.isTraining = true;
  }

  public addNewBlock() {
    this.requestBlocks.push({
      id: 0,
      heading: '',
      subheading: '',
      text: ''
    });
  }

  articleDto: ArticleDto = {
    id: 0,
    name: '',
    creatorId: JSON.parse(localStorage.getItem('user')!).userId,
    statusTypeId: 2,
    blocks: this.requestBlocks,
    creatorName: ''
  }

  recipeDto: RecipeDto = {
    id: 0,
    name: '',
    proteinsCount: 0,
    fatsCount: 0,
    carbsCount: 0,
    caloriesCount: 0,
    creatorId: JSON.parse(localStorage.getItem('user')!).userId,
    isDeleted: false,
    statusTypeId: 2,
    blocks: this.requestBlocks
  }

  trainingDto: TrainingDto = {
    id: 0,
    name: '',
    type: '',
    statusTypeId: 2,
    creatorId: JSON.parse(localStorage.getItem('user')!).userId,
    videoRef: '',
    isDeleted: false,
    blocks: []
  }

  createRequestDto: CreateRequestDto = {
    article:  null,
    recipe:  null,
    training:  null
  }
  
  public createRequest() {  
    if(this.isArticle) {
      this.createRequestDto.article = this.articleDto;
    }

    if(this.isRecipe)
      this.createRequestDto.recipe = this.recipeDto;

    if(this.isTraining)
      this.createRequestDto.training = this.trainingDto;

    console.log(this.createRequestDto);

    this.requestService.createRequest(this.createRequestDto).subscribe(x => console.log(x)); 
  }

  status: ChangeRequestDto = {
    id: 0,
    statusId: -2,
    comment: ''
  }

  public acceptRequest() {
    this.route.queryParams.subscribe(queryParam => {
      this.status.statusId = 6;
      this.status.id = queryParam['id'];
      this.requestService.changeStatus(this.status).subscribe(x => console.log(x));
   });
  }

  public cancelRequest() {
    this.route.queryParams.subscribe(queryParam => {
      this.status.statusId = 4;
      this.status.id = queryParam['id'];
      this.requestService.changeStatus(this.status).subscribe(x => console.log(x));
   });
  }

  public notAllRequest() {
    this.route.queryParams.subscribe(queryParam => {
      this.status.statusId = 3;
      this.status.id = queryParam['id'];
      this.requestService.changeStatus(this.status).subscribe(x => console.log(x));
   });
  }
}
