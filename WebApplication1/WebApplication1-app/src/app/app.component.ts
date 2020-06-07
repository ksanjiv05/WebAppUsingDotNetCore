import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `
   
    <div style="text-align:center" class="content">
      <h1>
        Welcome to {{title}}!
      </h1>
      <span style="display: block">{{ title }} app is running!</span>
     
    </div>
   
    
    <router-outlet></router-outlet>
  `,
  styles: []
})
export class AppComponent {
  title = 'WebApplication1-app';
}
