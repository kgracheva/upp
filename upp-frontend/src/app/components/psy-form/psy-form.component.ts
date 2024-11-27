import { Component } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/user.service';
import { StatusDto } from '../../models/StatusDto';

@Component({
  selector: 'app-psy-form',
  templateUrl: './psy-form.component.html',
  styleUrl: './psy-form.component.scss'
})
export class PsyFormComponent {
  questionsAnswers: number[] = [3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1];
  userAnswers: number[] = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

  constructor(private userService: UserService) {}

  selected(value: any, id: number) {
    this.userAnswers[id] = Number(value.target.value);
    console.log(this.userAnswers);
  }

  check(): boolean {
    let answersCount = 0;
    for(let i = 0; i < this.userAnswers.length; i++) {
      if(this.questionsAnswers[i] == this.userAnswers[i])
        answersCount += 1;
    }
    console.log(answersCount);
    if(answersCount >= 13)
      return true;

    return false;
  }

  accept() {
    if(this.check()) {
      let dto: StatusDto = {
        id: JSON.parse(localStorage.getItem('user')!).userId
      }
      this.userService.changeWorkStatus(dto).subscribe(x => console.log(x));
    }
  }
      
}

