import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/board.model';
import { Thread } from '../models/thread.model';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  private httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  /*

    getBoards()
    getThreadsByBoard()
    createThreadAndPost()
    createPost()
    deletePost()
    editPost()
    getPostsByThread()

  */

  getBoards(): Observable<Board[]> {
    return this.httpClient.get<Board[]>("https://localhost:7234/api/boards", {responseType: "json"});
  }

  getBoardAndThreads(id: string): Observable<BoardContext> {
    return this.httpClient.get<BoardContext>("https://localhost:7234/api/boards/" + id + "/threads", {responseType: "json"});
  }

}

export interface BoardContext {
  board: Board,
  threads: Thread[]
}
