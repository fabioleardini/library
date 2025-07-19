export enum OwnershipStatus {
  Own = 0,
  Love = 1,
  WantToRead = 2
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