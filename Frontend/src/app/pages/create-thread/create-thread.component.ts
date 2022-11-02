import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ThreadService } from 'src/app/services/thread.service';

@Component({
  selector: 'app-create-thread',
  templateUrl: './create-thread.component.html',
  styleUrls: ['./create-thread.component.css']
})
export class CreateThreadComponent {

  @Input()
  title!: string;
  @Input()
  content!: string;

  threadService: ThreadService;
  router: Router;
  boardId!: string;

  constructor(threadService: ThreadService, route: ActivatedRoute, router: Router){
    this.threadService = threadService;
    this.router = router;

    const id = route.snapshot.paramMap.get('id');
    if(id == null)
      return;

    this.boardId = id;
  }

  createThread(){
    this.threadService.createThread(this.boardId, this.title, this.content).subscribe({
      error: (e) => {
        console.log(e);
        alert("Kunde inte skapa en ny tråd, är du inloggad?")
      },
      next: (threadId) => {
        this.router.navigateByUrl("/threads/" + threadId);
      }
    })
  }

}
