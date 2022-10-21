import { Component, OnInit } from '@angular/core';
import { Board } from 'src/app/models/board.model';
import { BoardService } from 'src/app/services/board.service';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html',
  styleUrls: ['./boards.component.css']
})
export class BoardsComponent {

  boards?: Board[];

  constructor(boardService: BoardService) {
    boardService.getBoards().subscribe((v) => this.boards = v);
  } 

}