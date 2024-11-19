import { Component } from '@angular/core';
import { RecipeDto } from '../../models/RecipeDto';
import { EntityService } from '../../services/entity.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss'
})
export class RecipeComponent {
  public recipe: RecipeDto | undefined;

  constructor(private entityService: EntityService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.paramMap.subscribe(x => {
      this.entityService.getRecipe(x.get("id")).subscribe(art => {
        this.recipe = art;
      })
    })
  }
}
