export interface SentimentDto {
  sentiment: SentimentType;
  positiveScore: number;
  neutralScore: number;
  negativeScore: number;
}

export enum SentimentType {
  Positive = 0,
  Negative = 1,
  Neutral = 2,
  Empty = 4,
}
