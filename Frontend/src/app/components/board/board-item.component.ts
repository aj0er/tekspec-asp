import { Component, Input, OnInit } from '@angular/core';
import { Board } from 'src/app/models/board.model';

@Component({
  selector: 'app-board-item',
  templateUrl: './board-item.component.html',
  styleUrls: ['./board-item.component.css']
})
export class BoardItemComponent {

  @Input()
  board!: Board

  constructor() { }

}
