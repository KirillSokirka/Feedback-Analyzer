export const getFormatedDate = (date: Date) => {
  const createdDate = date instanceof Date ? date : new Date(date);

  return createdDate.toLocaleDateString("en-US", {
    month: "short",
    day: "numeric",
  });
};
