export interface UsersType {
    searchTerm: string;
    sortColumn: string;
    sortOrder: string;
    country?: string;
    accountCategory?: string;
    minFollowersCount?: number;
    minMediaCount?: number;
    pageNumber: number;
    pageSize: number;
}