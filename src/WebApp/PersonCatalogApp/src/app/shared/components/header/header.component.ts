import { Component,Output,EventEmitter  } from '@angular/core';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Output() toggleSidebar = new EventEmitter<void>(); // Emite evento para alternar sidebar

  onToggleSidebar() {
    this.toggleSidebar.emit(); // Emite el evento al padre para alternar el sidebar
  }
}
