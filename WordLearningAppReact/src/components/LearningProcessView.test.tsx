import { render, screen, fireEvent, waitFor, within } from "@testing-library/react";
import { LearningProcess } from "./LearningProcessView";
import { CardSet } from "../types/CardSet";
import "@testing-library/jest-dom";

const currentSet: CardSet = {
  id: "123",
  name: "Test Set",
  cards: [
    { id: "1", word: "Hello", translation: "Здравствуйте" },
    { id: "2", word: "Goodbye", translation: "До свидания" },
  ],
};

const handleBackToSetsMock = jest.fn();
const handleViewAllCards = jest.fn();

test("displays title and starting cards", () => {
  render(
    <LearningProcess
      currentSet={currentSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Assert
  expect(screen.getByText("Learning Process")).toBeInTheDocument();
  expect(screen.getByText("Hello")).toBeInTheDocument();
  expect(screen.queryByText("Здравствуйте")).not.toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Show Translation/i })).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Mark as Learned/i })).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Move to Bottom/i })).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /View All Cards/i })).toBeInTheDocument();
});

test("toggles translation appearance", () => {
  render(
    <LearningProcess
      currentSet={currentSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Show Translation/i }));

  // Assert
  expect(screen.getByText("Здравствуйте")).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Hide Translation/i })).toBeInTheDocument();

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Hide Translation/i }));

  // Assert
  expect(screen.queryByText("Здравствуйте")).not.toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Show Translation/i })).toBeInTheDocument();
});

test("marks card as learned and moves to next card", () => {
  const testSet: CardSet = {
    id: "1",
    name: "Test Set",
    cards: [{ id: "1", word: "Hello", translation: "Здравствуйте" }],
  };
  render(
    <LearningProcess
      currentSet={testSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Mark as Learned/i }));

  // Assert
  expect(screen.getByText("No cards to learn in this set.")).toBeInTheDocument();
});

test("moves card to bottom and shows next card", async () => {
  const testSet: CardSet = {
    id: "1",
    name: "Test Set",
    cards: [{ id: "1", word: "Hello", translation: "Здравствуйте" }],
  };
  render(
    <LearningProcess
      currentSet={testSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Move to Bottom/i }));

  // Assert
  expect(screen.queryByText("Hello")).toBeInTheDocument();
});

test("displays message when no cards to learn", () => {
  const testSet: CardSet = {
    id: "1",
    name: "Test Set",
    cards: [],
  };

  render(
    <LearningProcess
      currentSet={testSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Assert
  expect(screen.getByText("No cards to learn in this set.")).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Add New Card/i })).toBeInTheDocument();
});

test("navigates back to sets", () => {
  render(
    <LearningProcess
      currentSet={currentSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /Back to Sets/i }));

  // Assert
  expect(handleBackToSetsMock).toHaveBeenCalled();
});

test("navigates to view all cards", () => {
  render(
    <LearningProcess
      currentSet={currentSet}
      handleBackToSets={handleBackToSetsMock}
      handleViewAllCards={handleViewAllCards}
    />,
  );

  // Act
  fireEvent.click(screen.getByRole("button", { name: /View All Cards/i }));

  // Assert
  expect(handleViewAllCards).toHaveBeenCalled();
});
