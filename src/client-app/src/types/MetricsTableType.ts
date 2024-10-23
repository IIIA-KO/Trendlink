
export interface MetricsTableType {
    metrics: {
        name: string;
        values: Record<string, number>;
    }[];
}