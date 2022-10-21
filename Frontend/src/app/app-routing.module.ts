import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardComponent } from './pages/board/board.component';
import { BoardsComponent } from './pages/boards/boards.component';
import { ExampleComponent } from './pages/example/example.component';
import { ThreadComponent } from './pages/thread/thread.component';

const routes: Routes = [
  {path: "", component: ExampleComponent, pathMatch: "full"},
  {path: "boards", component: BoardsComponent},
  {path: "boards/:id", component: BoardComponent},
  {path: "threads/:id", component: ThreadComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
