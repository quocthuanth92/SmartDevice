function [loc1, loc2, match, OutputImage] = CheckMatchingTwoImages(scence, product)
    image1 = scence;
    image2 = product;
    [im1, des1, loc1] = sift(image1);
    [im2, des2, loc2] = sift(image2);
    distRatio = 0.6;  

    des2t = des2';                          % Precompute matrix transpose
    for i = 1 : size(des1,1)
       dotprods = des1(i,:) * des2t;        % Computes vector of dot products
       [vals,indx] = sort(acos(dotprods));  % Take inverse cosine and sort results

       % Check if nearest neighbor has angle less than distRatio times 2nd.
       if (vals(1) < distRatio * vals(2))
          match(i) = indx(1);
       else
          match(i) = 0;
       end
    end

    position = [];
    for i = 1 : size(loc1,1)
        if(match(i) > 0)
            y = loc1(i,1);
            x = loc1(i,2);
            position = [position ; [x y]];
        end
    end
    OutputImage = insertMarker(im1, position, '*', 'Size' , 10);
    num = sum(match > 0);
    fprintf('Found %d matches.\n', num);
end
