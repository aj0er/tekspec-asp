import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Board } from 'src/app/models/board.model';
import { BoardContext, BoardService } from 'src/app/services/board.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {

  boardContext?: BoardContext

  constructor(route: ActivatedRoute, boardService: BoardService) {
    const id = route.snapshot.paramMap.get('id');
    if(id == null)
      return;

    boardService.getBoardAndThreads(id).subscribe((v) => this.boardContext = v);
  }

}
