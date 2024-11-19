import { Component, Input } from '@angular/core';
import { ArticleDto } from '../../models/ArticleDto';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrl: './article-card.component.scss'
})
export class ArticleCardComponent {
  @Input() article!: ArticleDto;
}
