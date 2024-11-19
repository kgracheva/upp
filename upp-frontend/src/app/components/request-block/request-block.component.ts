import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-request-block',
  templateUrl: './request-block.component.html',
  styleUrl: './request-block.component.scss'
})
export class RequestBlockComponent {
  public isSubHead: boolean = false;

  @Input() heading: string = "";
  @Input() subHeading: string = "";
  @Input() text: string = "";
  @Input() id: number = 0;

  public addSubheading() {
    if(this.isSubHead) {
      this.subHeading = "";
      this.isSubHead = false; 
    }
    else {
      this.isSubHead = true; 
    }
  }

  @Output() headingChange = new EventEmitter<string>();
  onHeadingChange(model: string){
       
      this.heading = model;
      this.headingChange.emit(model);
  }

  @Output() subHeadingChange = new EventEmitter<string>();
  onSubheadingChange(model: string){
       
      this.subHeading = model;
      this.subHeadingChange.emit(model);
  }

  @Output() textChange = new EventEmitter<string>();
  onTextChange(model: string){
       
      this.text = model;
      this.textChange.emit(model);
  }
}
