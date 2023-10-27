import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RebelConsoleComponent } from './components/rebel-console/rebel-console.component';
import { ImperialConsoleComponent } from './components/imperial-console/imperial-console.component';


const routes: Routes = [
  {path: '', component: ImperialConsoleComponent},
  {path: 'imperialConsole', component: ImperialConsoleComponent},
  {path: 'rebelConsole', component: RebelConsoleComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
