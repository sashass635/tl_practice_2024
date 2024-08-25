import "@testing-library/jest-dom";
import { render, screen, fireEvent } from "@testing-library/react";
import { AllCards } from "./AllCards";
import { CardSet } from "../types/CardSet";

const currentSet: CardSet = {
  id: "123",
  name: "Test Set",
  cards: [],
};

const Application = {
  cardsSet: [currentSet],
};

const setCurrentSetMock = jest.fn();
const setApplicationMock = jest.fn();
const handleBackToLearningMock = jest.fn();

test("displays title and input fields", () => {
  render(
    <AllCards
      currentSet={currentSet}
      setCurrentSet={setCurrentSetMock}
      application={Application}
      setApplication={setApplicationMock}
      handleBackToLearning={handleBackToLearningMock}
    />,
  );

  // Assert
  expect(screen.getByText("Test Set - All Cards")).toBeInTheDocument();
  expect(screen.getByPlaceholderText("New Word")).toBeInTheDocument();
  expect(screen.getByPlaceholderText("Translation")).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Add Card/i })).toBeInTheDocument();
});

test("adds a new card", () => {
  render(
    <AllCards
      currentSet={currentSet}
      setCurrentSet={setCurrentSetMock}
      application={Application}
      setApplication={setApplicationMock}
      handleBackToLearning={handleBackToLearningMock}
    />,
  );

  // Act
  fireEvent.change(screen.getByPlaceholderText("New Word"), { target: { value: "flower" } });
  fireEvent.change(screen.getByPlaceholderText("Translation"), { target: { value: "цветок" } });
  fireEvent.click(screen.getByRole("button", { name: /Add Card/i }));

  // Assert
  expect(setCurrentSetMock).toHaveBeenCalled();
  expect(setApplicationMock).toHaveBeenCalled();
  expect(screen.getByPlaceholderText("New Word")).toHaveValue("");
  expect(screen.getByPlaceholderText("Translation")).toHaveValue("");
});

test("edits an existing card", () => {
  const cardSetWithCards = {
    ...currentSet,
    cards: [{ id: "1", word: "fine", translation: "хорошо" }],
  };

  render(
    <AllCards
      currentSet={cardSetWithCards}
      setCurrentSet={setCurrentSetMock}
      application={Application}
      setApplication={setApplicationMock}
      handleBackToLearning={handleBackToLearningMock}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Edit/i }));
  fireEvent.change(screen.getByPlaceholderText("New Word"), { target: { value: "fine" } });
  fireEvent.change(screen.getByPlaceholderText("Translation"), { target: { value: "штраф" } });

  // Assert
  expect(setCurrentSetMock).toHaveBeenCalled();
  expect(setApplicationMock).toHaveBeenCalled();
});

test("deletes an existing card", () => {
  const cardSetWithCards = {
    ...currentSet,
    cards: [{ id: "1", word: "flower", translation: "цветок" }],
  };

  render(
    <AllCards
      currentSet={cardSetWithCards}
      setCurrentSet={setCurrentSetMock}
      application={Application}
      setApplication={setApplicationMock}
      handleBackToLearning={handleBackToLearningMock}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Delete/i }));

  // Assert
  expect(setCurrentSetMock).toHaveBeenCalled();
  expect(setApplicationMock).toHaveBeenCalled();
});
