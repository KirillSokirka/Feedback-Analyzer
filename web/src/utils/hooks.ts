import { useDebouncedCallback } from "use-debounce";
import { useEffect } from "react";

type CallbackFunction = (...args: any[]) => any;

const useDebouncedApiCall = (callback: CallbackFunction, delay: number) => {
  const debouncedCallback = useDebouncedCallback(callback, delay);

  useEffect(() => {
    return () => debouncedCallback.cancel();
  }, [debouncedCallback]);

  return debouncedCallback;
};

export default useDebouncedApiCall;
