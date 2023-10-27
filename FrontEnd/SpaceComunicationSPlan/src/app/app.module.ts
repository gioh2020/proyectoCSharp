import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RebelConsoleComponent } from './components/rebel-console/rebel-console.component';
import { ImperialConsoleComponent } from './components/imperial-console/imperial-console.component';

@NgModule({
  declarations: [
    AppComponent,
    RebelConsoleComponent,
    ImperialConsoleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
