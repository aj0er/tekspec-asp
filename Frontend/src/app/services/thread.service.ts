import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Post } from '../models/post.model';
import { Thread } from '../models/thread.model';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {

  private httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  getThreadAndPosts(id: string): Observable<ThreadContext> {
    return this.httpClient.get<ThreadContext>("https://localhost:7234/api/threads/" + id + "/posts", {responseType: "json"});
  }

  createPost(threadId: string, content: string): Observable<Post[]> {
    return this.httpClient.post<Post[]>("https://localhost:7234/api/threads/" + threadId + "/posts", {
      content
    }, {
      withCredentials: true
    });
  }

}

export interface ThreadContext {
  thread: Thread,
  posts: Post[]
}
