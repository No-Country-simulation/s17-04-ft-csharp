export interface Offer {
  id?: number;
  title: string;
  description: string;
  price: number;
  estimatedTime: number;
  state: number;
  difficult: number;
  technology: string[];
}
