import { toast } from "react-toastify";
import { ApiResponse, ProblemDetails } from "./interfaces";

export const processResponse = <T>(response: ApiResponse<T>): T | null => {
  if ("data" in response && response.data !== undefined) {
    return response.data;
  } else {
    if ("problemDetails" in response && response.problemDetails !== undefined) {
      console.log(response.problemDetails);

      const error = response.problemDetails.errors?.at(0);
      const message = error
        ? `${error.code}: ${error.description}`
        : "An error occurred.";

      toast.error(message);
    } else {
      toast.error("An unknown error occurred.");
    }

    return null;
  }
};
