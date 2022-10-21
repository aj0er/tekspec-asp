import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Post } from 'src/app/models/post.model';

@Component({
  selector: 'app-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css']
})
export class PostItemComponent {

  @Input()
  post!: Post
  edit: Post|null|undefined

  @Output() deleted = new EventEmitter<{ id: string }>();
  @Output() saved = new EventEmitter<Post>();

  startEdit(){
    if(this.edit == null && this.post != null){
      this.edit = {... this.post};
    }
  }

  save(){
    if(this.edit == null)
      return;

    this.post = {... this.edit};
    this.saved.emit(this.edit);
    this.edit = null;
  }

  deletePost(){
    if(!confirm("Vill du verkligen ta bort detta inlägg?"))
      return;

    this.deleted.emit({ id: this.post.id });
  }

  cancel(){
    this.edit = null;
  }

} 
