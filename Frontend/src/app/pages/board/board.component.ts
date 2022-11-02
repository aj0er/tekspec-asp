import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BoardContext, BoardService } from 'src/app/services/board.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {

  boardContext?: BoardContext
  private router: Router

  private boardId!: string;

  constructor(route: ActivatedRoute, boardService: BoardService, router: Router) {
    this.router = router;
    const id = route.snapshot.paramMap.get('id');
    if(id == null)
      return;

    this.boardId = id;

    boardService.getBoardAndThreads(id).subscribe((v) => this.boardContext = v);
  }

  createThread(){
    this.router.navigateByUrl("/boards/" + this.boardId + "/createThread");
  }

}
