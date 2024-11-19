import { Component } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { FindRequestsDto } from '../../models/FindRequestsDto';
import { RequestDto } from '../../models/Request';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.scss'
})
export class RequestsComponent {
  displayedColumns: string[] = ['type', 'name', 'status', 'date', 'actions'];
  dataSource: RequestDto[] = [];



  findRequests: FindRequestsDto = {
    creatorId: JSON.parse(localStorage.getItem('user')!).userId,
    requestType: 1,
    name: '',
    page: 0,
    size: 0,
    date: null
  };
 
  constructor(private requestService: RequestService) {
    this.getRequests();
  }

  public getRequests() {
    this.requestService.getRequests(this.findRequests).subscribe(r => { this.dataSource = r.items; console.log(this.dataSource) });
  }

  public getWorkType(num: string) {
    console.log(num);
    if(num == '1') {
      return "Статья";
    }
    
    if(num == '2') {
      return "Рецепт";
    }

    if(num == '3') {
      return "Тренировка"; 
    }

    return "Ошибка"; 
  }
}
