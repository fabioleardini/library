export enum OwnershipStatus {
  Own = 'Own',
  Love = 'Love',
  WantToRead = 'WantToRead'
}

export interface Book {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  totalCopies: number;
  copiesInUse: number;
  type: string;
  isbn: string;
  category: string;
  status: OwnershipStatus;
  author: string;
  availableCopies: number;
}