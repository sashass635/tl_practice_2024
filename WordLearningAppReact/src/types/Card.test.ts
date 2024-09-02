import { createCard, updateCard, deleteCard, Card } from "./Card";

describe("Card", () => {
  describe("CreateCard", () => {
    it("should create a card", () => {
      const word = "Hello";
      const translation = "Здравствуйте";
      const newCard: Card = createCard(word, translation);

      expect(newCard).toEqual({
        id: expect.any(String),
        word: word,
        translation: translation,
      });
    });
  });

  describe("UpdateCard", () => {
    it("should update the translation of a card", () => {
      const cards: Card[] = [{ id: "1", word: "fine", translation: "хорошо" }];
      const updatedCards = updateCard(cards, "1", undefined, "штраф");

      expect(updatedCards).toEqual([{ id: "1", word: "fine", translation: "штраф" }]);
    });

    it("should update the word of a card", () => {
      const cards: Card[] = [{ id: "2", word: "blue", translation: "печальный" }];
      const updatedCards = updateCard(cards, "2", "sad", undefined);

      expect(updatedCards).toEqual([{ id: "2", word: "sad", translation: "печальный" }]);
    });

    it("should not update any card if the id does not match", () => {
      const cards: Card[] = [{ id: "2", word: "blue", translation: "печальный" }];
      const updatedCards = updateCard(cards, "3", "happy", "счастливый");

      expect(updatedCards).toEqual(cards);
    });
  });

  describe("DeleteCard", () => {
    it("should delete a card from the list", () => {
      const cards: Card[] = [{ id: "1", word: "fine", translation: "хорошо" }];
      const updatedCards = deleteCard(cards, "1");

      expect(updatedCards.find((card) => card.id === "1")).toBeUndefined();
      expect(updatedCards).toHaveLength(0);
    });

    it("should not delete any card if the id does not match", () => {
      const cards: Card[] = [{ id: "1", word: "fine", translation: "хорошо" }];
      const updatedCards = deleteCard(cards, "2");

      expect(updatedCards).toHaveLength(1);
      expect(updatedCards).toEqual(cards);
    });
  });
});
