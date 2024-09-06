export interface Offer {
  id?: number;
  title: string;
  description: string;
  price: number;
  estimatedTime: number;
  state: number;
  difficult: number;
  technology: Technology[];
}

export interface Technology {
  id: number;
  name: string;
}
