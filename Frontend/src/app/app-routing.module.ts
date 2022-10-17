import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BoardsComponent } from './boards/boards.component';
import { ExampleComponent } from './example/example.component';

const routes: Routes = [
  {path: "", component: ExampleComponent, pathMatch: "full"},
  {path: "boards", component: BoardsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
