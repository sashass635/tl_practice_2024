import { render, screen, fireEvent } from "@testing-library/react";
import { ManageCardSets } from "./ManageCardSets";
import "@testing-library/jest-dom";

const Application = {
  cardsSet: [
    { id: "1", name: "Set1", cards: [] },
    { id: "2", name: "Set2", cards: [] },
  ],
};

const setApplicationMock = jest.fn();
const handleSelectSetMock = jest.fn();

test("displays title and input field", () => {
  render(
    <ManageCardSets
      application={Application}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Assert
  expect(screen.getByText("Card Sets Management")).toBeInTheDocument();
  expect(screen.getByPlaceholderText("Enter new set name")).toBeInTheDocument();
  expect(screen.getByRole("button", { name: /Add Set/i })).toBeInTheDocument();
});

test("displays card sets", () => {
  render(
    <ManageCardSets
      application={Application}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Assert
  expect(screen.getByText("Set1")).toBeInTheDocument();
  expect(screen.getByText("Set2")).toBeInTheDocument();
  expect(screen.getAllByRole("button", { name: /Start Learning Process/i })).toHaveLength(2);
  expect(screen.getAllByRole("button", { name: /Delete Set/i })).toHaveLength(2);
});

test("adds a new card set", () => {
  render(
    <ManageCardSets
      application={Application}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Act
  fireEvent.change(screen.getByPlaceholderText("Enter new set name"), { target: { value: "Animals" } });
  fireEvent.click(screen.getByRole("button", { name: /Add Set/i }));

  // Assert
  expect(setApplicationMock).toHaveBeenCalled();
  expect(screen.getByPlaceholderText("Enter new set name")).toHaveValue("");
});

test("deletes a card set", () => {
  render(
    <ManageCardSets
      application={Application}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Act
  fireEvent.click(screen.getAllByRole("button", { name: /Delete Set/i })[0]);

  // Assert
  expect(setApplicationMock).toHaveBeenCalled();
});

test("selects a card set", () => {
  render(
    <ManageCardSets
      application={Application}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Act
  fireEvent.click(screen.getAllByRole("button", { name: /Start Learning Process/i })[0]);

  // Assert
  expect(handleSelectSetMock).toHaveBeenCalledWith(Application.cardsSet[0]);
});

test("displays message when no card sets are available", () => {
  const emptyApplication = { cardsSet: [] };

  render(
    <ManageCardSets
      application={emptyApplication}
      setApplication={setApplicationMock}
      handleSelectSet={handleSelectSetMock}
    />,
  );

  // Assert
  expect(screen.getByText("No card sets available.")).toBeInTheDocument();
});
