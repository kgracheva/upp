import { Component } from '@angular/core';
import { TrainingDto } from '../../models/TrainingDto';
import { EntityService } from '../../services/entity.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrl: './training.component.scss'
})
export class TrainingComponent {
  public training: TrainingDto | undefined;

  constructor(private entityService: EntityService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.paramMap.subscribe(x => {
      this.entityService.getTraining(x.get("id")).subscribe(art => {
        this.training = art;
      })
    })
  }
}
