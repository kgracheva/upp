import { Component } from '@angular/core';
import { BlockDto } from '../../models/BlockDto';
import { RequestService } from '../../services/request.service';
import { ArticleDto } from '../../models/ArticleDto';
import { CreateRequestDto } from '../../models/CreateRequestDto';
import { RecipeDto } from '../../models/RecipeDto';
import { TrainingDto } from '../../models/TrainingDto';

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

  constructor(private requestService: RequestService) {
   this.addNewBlock();
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
}
