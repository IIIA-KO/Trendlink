export interface CooperationType {
    id: string;
    name: string;
    description: string;
    scheduledOnUtc: string;
    priceAmount: number;
    priceCurrency: string;
    advertisementId: string;
    buyerId: string;
    sellerId: string;
    status: number;
}