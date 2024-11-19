import { Component } from '@angular/core';
import { EntityService } from '../../services/entity.service';
import { ArticleDto } from '../../models/ArticleDto';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrl: './articles.component.scss'
})
export class ArticlesComponent {

  articlesList: ArticleDto[] = []; 
  constructor(private entityService: EntityService) {
    this.getArticles();
  }

  getArticles() {
    this.entityService.getArticles().subscribe(x => this.articlesList = x.items);
  }
}
