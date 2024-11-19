import { Component } from '@angular/core';
import { EntityService } from '../../services/entity.service';
import { ActivatedRoute } from '@angular/router';
import { ArticleDto } from '../../models/ArticleDto';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss'
})
export class ArticleComponent {
  public article: ArticleDto | undefined;

  constructor(private entityService: EntityService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.paramMap.subscribe(x => {
      this.entityService.getArticle(x.get("id")).subscribe(art => {
        this.article = art;
      })
    })
  }
}
