import { Component } from '@angular/core';
import { EntityService } from '../../services/entity.service';
import { RecipeDto } from '../../models/RecipeDto';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss'
})
export class RecipesComponent {
  recipesList: RecipeDto[] = [];
  constructor(private entityService: EntityService) {
    this.getArticles();
  }

  getArticles() {
    this.entityService.getRecipes().subscribe(x => this.recipesList = x.items);
  }
}
