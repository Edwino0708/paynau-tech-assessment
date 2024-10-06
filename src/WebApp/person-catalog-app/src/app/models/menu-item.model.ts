export interface MenuItem {
  name: string;
  route: string;
  icon: string;
  isOpen?: boolean;
  subMenu?: MenuItem[];
  isActive?: boolean;
}

