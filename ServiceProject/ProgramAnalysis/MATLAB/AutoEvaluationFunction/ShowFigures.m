% I1 = THMILK_AllImages{132,3};
% BW1 = im2bw(I1, graythresh(I1)); 
% ShowFigures(1,2, {I1 BW1}, {'' ''});
function ShowFigures (rows, cols, CellData, CellDescr)
    figure;
    for i = 1 : rows * cols
        subplot(rows,cols, i);
        imshow(CellData{i},[]);
        if nargin == 4
            title(CellDescr{i});
        end
    end
end