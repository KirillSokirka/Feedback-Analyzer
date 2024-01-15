import { toast } from "react-toastify";
import { ApiResponse } from "./interfaces";

export const processResponse = <T>(
  response: ApiResponse<T>
): T | null | boolean => {
  if ("data" in response) {
    return response.data !== undefined ? response.data : true;
  } else {
    if ("problemDetails" in response && response.problemDetails !== undefined) {
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
