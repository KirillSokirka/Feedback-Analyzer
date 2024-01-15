import { SentimentType } from "../api/statitstic";

export const getEmoji = (sentiment: SentimentType) => {
  switch (sentiment) {
    case SentimentType.Positive:
      return "😊";
    case SentimentType.Negative:
      return "😞";
    case SentimentType.Neutral:
      return "😐";
    default:
      return "Empty";
  }
};

export const getText = (sentiment: SentimentType) => {
  switch (sentiment) {
    case SentimentType.Positive:
      return "positive";
    case SentimentType.Negative:
      return "negative";
    case SentimentType.Neutral:
      return "neutral";
    default:
      return "Empty";
  }
};
