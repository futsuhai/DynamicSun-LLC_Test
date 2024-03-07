import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  host: {
    class: 'sidebar-component'
  }
})
export class SidebarComponent {

  public currentRout: string = "";

  constructor(private router: Router) { }

  public isRouteActive(rout: string): boolean {
    this.currentRout = this.router.url;
    return this.currentRout === rout ? true : false;
  }
}
