import { Component, Input, OnInit } from '@angular/core';
import { Thread } from 'src/app/models/thread.model';

@Component({
  selector: 'app-thread-item',
  templateUrl: './thread-item.component.html',
  styleUrls: ['./thread-item.component.css']
})
export class ThreadItemComponent {

  @Input()
  thread!: Thread

  constructor() { }

}
