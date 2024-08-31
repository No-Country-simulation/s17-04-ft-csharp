import { ImageList, ImageListItem } from "@mui/material";
import { useAppSelector } from "../../../hooks/hooks";

export default function StandardImageList() {
  const { offerActive: noteActive } = useAppSelector((state) => state.offer);
  if (!noteActive || noteActive.imageURLs === undefined)
    throw new Error("$noteActive is empty");

  return (
    <ImageList sx={{ width: "100%", height: 500 }} cols={3} rowHeight={250}>
      {noteActive?.imageURLs.map((image) => (
        <ImageListItem key={image}>
          <img
            srcSet={`${image}?w=164&h=164&fit=crop&auto=format&dpr=2 2x`}
            src={`${image}?w=164&h=164&fit=crop&auto=format`}
            alt="Images' note"
            loading='lazy'
          />
        </ImageListItem>
      ))}
    </ImageList>
  );
}
