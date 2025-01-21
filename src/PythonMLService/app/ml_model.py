from transformers import pipeline

class SentimentAnalyzer:
    def __init__(self):
        # Load the sentiment analysis pipeline
        self.analyzer = pipeline("sentiment-analysis")

    async def analyze(self, text: str):
        result = self.analyzer(text)[0]
        return {
            "sentiment": "Positive" if result["label"] == "POSITIVE" else "Negative",
            "score": result["score"]
        }
