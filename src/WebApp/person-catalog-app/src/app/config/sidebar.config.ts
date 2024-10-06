import { MenuItem } from '../models/menu-item.model';

export const menuItems: MenuItem[] = [
  { name: 'Inicio', route: '/home', icon: 'home', isActive: false },
  {
    name: 'Servicios',
    route: '',
    icon: 'build',
    isOpen: false,
    subMenu: [
      { name: 'Persona', route: 'services/person/', icon: 'person', isActive: false },
    ],
  },
  // { name: 'Sobre mi', route: '/about', icon: 'info', isActive: false },
];
