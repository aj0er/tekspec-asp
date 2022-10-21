import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Post } from 'src/app/models/post.model';
import { PostService } from 'src/app/services/post.service';
import { ThreadContext, ThreadService } from 'src/app/services/thread.service';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrls: ['./thread.component.css']
})
export class ThreadComponent {

  threadContext?: ThreadContext

  private postService: PostService
  private threadService: ThreadService

  @Input()
  createPostContent: string = ""

  creatingPost: boolean = false  

  constructor(route: ActivatedRoute, threadService: ThreadService, postService: PostService) {
    this.postService = postService;
    this.threadService = threadService;

    const id = route.snapshot.paramMap.get('id');
    if(id == null)
      return;

    this.threadService
        .getThreadAndPosts(id)
        .subscribe((v) => this.threadContext = v);    
  }

  async createPost(){
    if(this.threadContext == null)
      return;

    if(!this.creatingPost){
      this.creatingPost = true;
      return;
    }

    this.threadService.createPost(this.threadContext.thread.id, this.createPostContent).subscribe(posts => {
      if(this.threadContext == null)
        return;

      this.threadContext.posts = posts;
      this.creatingPost = false;
      this.createPostContent = "";
    });
  }

  cancelCreate(){
    this.creatingPost = false;
  }

  async onPostSaved(post: Post) {
    if(this.threadContext == null)
      return;

    const request = {
      content: post.content
    }

    let saved = await lastValueFrom(this.postService.saveEditedPost(post.id, request));
    if(!saved){
      alert("Ett fel uppstod!");
    }
  }

  async onPostDeleted(event: { id: string; }) {
    if(this.threadContext == null)
      return;

      let deleted = await lastValueFrom(this.postService.deletePost(event.id));
      if(deleted){
        this.threadContext.posts = this.threadContext.posts.filter(p => p.id != event.id);
      } else {
        alert("Ett fel uppstod!")
      }
  }

}
