import React, { useRef, useEffect } from "react";

import { ContentInput } from "./styles";

interface ContentTextAreaProps {
  placeHolder: string;
  value: string;
  handleChange: (value: string) => void;
}

const ContentTextArea = ({
  placeHolder,
  value,
  handleChange,
}: ContentTextAreaProps) => {
  const textAreaRef = useRef<HTMLTextAreaElement>(null);

  useEffect(() => {
    const adjustHeight = () => {
      if (textAreaRef.current) {
        textAreaRef.current.style.height = "auto";
        textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
      }
    };

    adjustHeight();
    window.addEventListener("resize", adjustHeight);

    adjustHeight();

    return () => window.removeEventListener("resize", adjustHeight);
  }, [value]);

  const handleTextChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    handleChange(event.target.value);
    if (textAreaRef.current) {
      textAreaRef.current.style.height = "auto";
      textAreaRef.current.style.height = `${event.target.scrollHeight}px`;
    }
  };

  return (
    <ContentInput
      ref={textAreaRef}
      value={value}
      onChange={handleTextChange}
      placeholder={placeHolder}
    />
  );
};

export default ContentTextArea;
