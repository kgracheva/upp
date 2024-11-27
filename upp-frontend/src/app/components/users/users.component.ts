import { Component } from '@angular/core';
import { User } from '../../models/User';
import { Subscription, delay, of } from 'rxjs';
import { UserService } from '../../services/user.service';
import { PhyschologistDto } from '../../models/PhyschologistDto';
import { StatusDto } from '../../models/StatusDto';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {
  public users: PhyschologistDto[] = [];
  public displayedColumns: string[] = [
    'id',
    'name',
    'date',
    'actions'
  ];
  public canManage = false;
  public defaultSize: number = 20;

  public isDeleted = false;
  public isMobile = false;

  public totalCount: number = 0;
  public loadTimeout: Subscription | null = null;
  public searchModel: {
    fio: string;
    date: Date | null;
    page: number;
    size: number;
  } = {
    fio: '',
    date: new Date(),
    page: 1,
    size: this.defaultSize,
  };

  constructor(
    public userService: UserService
  ) {
    // this.clearFilters();
  }

  changeStatus(id: number) {
    let dto: StatusDto = {
      id: id
    }
    this.userService.changeWorkStatus(dto).subscribe(x => console.log(x));
  }

  ngOnInit(): void {
    // this.loadRoles();
    this.getPasses();

    this.createDelayObservable();
    // setInterval(() => { this.getPasses(); console.log('request'); }, 25000);

    window.addEventListener('click', () => this.mouseClick());
  }

  private createDelayObservable() {
    this.loadTimeout = of(123)
      .pipe(delay(1000 * 60 * 5))
      .subscribe((x) => {
        // this.clearFilters();
        this.mouseClick();
      });
  }

  public mouseClick() {
    this.loadTimeout!.unsubscribe();
    this.loadTimeout = null;
    console.log('aaaa');
    this.createDelayObservable();
  }

  public getPasses() {
    let date = null;

    // console.log(date);

    this.userService
      .getUsers()
      .subscribe((response) => {
        console.log(response);
        this.users = response.items;
        this.totalCount = response.totalCount;
      });
  }

  // public openCreatingModal() {
  //   const dialogRef = this.dialog.open(PassModalComponent, {
  //     data: {
  //       modalTitle: this.creatingModalTitle,
  //       pass: {},
  //       modalButtonName: this.creatingModalButtonName,
  //     },
  //   });

  //   dialogRef.afterClosed().subscribe((_) => {
  //     // this.getUsers();
  //     this.getPasses();
  //   });
  // }

  // public openImportingModal() {
  //   const dialogRef = this.dialog.open(ImportModalComponent, {});

  //   dialogRef.afterClosed().subscribe((_) => {
  //     // this.getUsers();
  //     this.getPasses();
  //   });
  // }

  // public afterPassDeleted() {
  //   this.getPasses();
  // }

  // public getStatus(pass: Pass) {
  //   // console.log('aaaa');

  //   if (pass.userActions?.length == 0) {
  //     return 'Еще не вошел';
  //   }

  //   // console.log(pass);

  //   const lastAction = pass.userActions!.reduce(function (prev, current) {
  //     return prev && prev.created! > current.created! ? prev : current;
  //   });

  //   if (lastAction?.type === UserActionType.Enter) {
  //     return 'Вошел';
  //   }

  //   if (lastAction?.type === UserActionType.Exit) {
  //     return 'Вышел';
  //   }
  //   return '';
  // }

  // public lastStatus(pass: Pass) {
  //   if (pass.userActions?.length == 0) {
  //     return null;
  //   }
  //   const lastAction = pass.userActions!.reduce(function (prev, current) {
  //     // console.log(prev, current);
  //     return prev && prev.created! > current.created! ? prev : current;
  //   });
  //   return lastAction?.type;
  // }

  public filterChange() {
    this.searchModel.page = 1;
    // this.searchModel.size = 5;

    // console.log(this.searchModel);

    this.getPasses();
  }

  // private loadRoles() {
  //   const roles = this.authService.getRoles();

  //   if (roles.includes(Roles.Admin) || roles.includes(Roles.Moderator)) {
  //     this.canManage = true;
  //   }
  //   // console.log(this.canManage);
  // }

  // public changeShowDeleted() {
  //   this.isDeleted = !this.isDeleted;
  //   this.searchModel.isDeleted = this.isDeleted;
  //   this.searchModel.page = 1;
  //   // this.searchModel.size = 5;

  //   this.filterChange();
  // }

  // public export() {
  //   let date = null;

  //   if (this.searchModel.date != null) {
  //     date = this.searchModel.date;
  //     date.setHours(12);
  //   }

  //   let url_ = `${environment.uri}` + '/api/Pass/export?';
  //   if (date !== undefined && date !== null)
  //     url_ +=
  //       'Date=' + encodeURIComponent(date ? '' + date.toISOString() : '') + '&';
  //   if (this.searchModel.fio !== undefined && this.searchModel.fio !== null)
  //     url_ += 'Fio=' + encodeURIComponent('' + this.searchModel.fio) + '&';
  //   url_ += `isDeleted=${this.searchModel.isDeleted}`;

  //   window.location.href = url_;
  // }

  // public getServerData(event?: any) {
  //   this.searchModel.page = event.currentPage;
  //   this.searchModel.size = event.size;
  //   // console.log(this.searchModel);

  //   this.getPasses();
  // }

  // private checkIsMobile() {
  //   if (window.innerWidth <= 768) {
  //     this.isMobile = true;
  //   } else {
  //     this.isMobile = false;
  //   }
  // }

  // // public formatFio(pass: Pass) {
  // //   let fio = pass.user?.info?.fio;
  // //   if(this.isMobile) {
  // //     let fioParts = fio.split(' ');

  // //     let formattedFio = fioParts[0] + ' ' + fioParts[1][0] + '. ' + fioParts[2][0] + '.';

  // //     return formattedFio;
  // //   }
  // //   return fio;
  // // }
  // public openMobileViewModal(pass: Pass) {
  //   if (this.isMobile) {
  //     const dialogRef = this.dialog.open(PassModalMobileComponent, {
  //       data: pass,
  //     });

  //     dialogRef.afterClosed().subscribe((_) => {});
  //   }
  // }

  // public formatFio(pass: Pass) {
  //   var userInfo = pass.user.info;

  //   var fio = userInfo.surname + ' ' + userInfo.name;

  //   if (userInfo.patronymic != null) {
  //     fio += ' ' + userInfo.patronymic;
  //   }

  //   return fio;
  // }

  // public formatCarNumber(number: string) {
  //   if (number == null) {
  //     return 'Номер отсутствует';
  //   } else {
  //     return number;
  //   }
  // }

  // public isMultiDays(pass: Pass) {
  //   // console.log()
  //   return pass.from.toDateString() != pass.to.toDateString();
  // }

  // public addPlacesToTable() {
  //   var dtos = [
  //     new AddPlaceDto({ name: 'Корпус 1' }),
  //     new AddPlaceDto({ name: 'Корпус 2' }),
  //     new AddPlaceDto({ name: 'Корпус 3' }),
  //     new AddPlaceDto({ name: 'Корпус 4' }),
  //     new AddPlaceDto({ name: 'Корпус 5' }),
  //   ];

  //   dtos.forEach((x) => {
  //     this.placeClient.createPlace(x).subscribe((_) => _);
  //   });
  // }

  // public isLastOfPlacesList(pass: Pass, dto: PassPlace) {
  //   return pass.passPlaces[pass.passPlaces.length - 1] == dto;
  // }

  // public getPassCreator(pass: Pass) {
  //     of(123).subscribe(x => this.userClient
  //       .getUserById(pass.creatorId))

  // }

  // public clearFilters() {
  //   this.searchModel.carNumber = null;
  //   this.searchModel.fio = '';
  //   (this.searchModel.date = new Date()),
  //     (this.searchModel.isDeleted = false),
  //     (this.searchModel.page = 1),
  //     (this.searchModel.size = this.defaultSize),
  //     (this.searchModel.carNumber = null),
  //     (this.searchModel.target = null);
  //   this.filterChange();
  // }
}
