import { Component } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { FindRequestsDto } from '../../models/FindRequestsDto';
import { RequestDto } from '../../models/Request';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.scss'
})
export class RequestsComponent {
  displayedColumns: string[] = ['type', 'name', 'status', 'date', 'actions'];
  dataSource: RequestDto[] = [];

  isAdmin: boolean = false;

  findRequests: FindRequestsDto = {
    creatorId: 0,
    requestType: 0,
    name: '',
    page: 0,
    size: 0,
    date: null
  };

  constructor(private requestService: RequestService, private userService: AuthService) {
    this.isAdmin = this.userService.getRoles().lastIndexOf("Admin") == -1 ? false : true;
    this.getRequests();
  }

  public getRequests() {
    if(!this.isAdmin) {
      this.findRequests.creatorId = JSON.parse(localStorage.getItem('user')!).userId;
    }

    this.requestService.getRequests(this.findRequests).subscribe(r => { this.dataSource = r.items; });
  }

  public getWorkType(num: string) {
    if(num == '0') {
      return "Статья";
    }

    if(num == '1') {
      return "Рецепт";
    }

    if(num == '2') {
      return "Тренировка";
    }

    return "Ошибка";
  }
}
