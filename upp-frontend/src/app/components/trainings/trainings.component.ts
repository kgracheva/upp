import { Component } from '@angular/core';
import { TrainingDto } from '../../models/TrainingDto';
import { EntityService } from '../../services/entity.service';

@Component({
  selector: 'app-trainings',
  templateUrl: './trainings.component.html',
  styleUrl: './trainings.component.scss'
})
export class TrainingsComponent {
  trainingsList: TrainingDto[] = [];
  constructor(private entityService: EntityService) {
    this.getArticles();
  }

  getArticles() {
    this.entityService.getTrainings().subscribe(x => this.trainingsList = x.items);
  }
}
