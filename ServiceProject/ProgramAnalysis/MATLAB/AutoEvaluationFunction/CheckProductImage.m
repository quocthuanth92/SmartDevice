function [ Result, Rate, RunningTime, ErrorStatus] = CheckProductImage( ImagePath, ProductImagePath)
%CHECKPRODUCTIMAGE Summary of this function goes here
%   Detailed explanation goes here
	ErrorStatus = ' ';
    warning('off','all');
    curdir=cd;
    try
        I = imread(ImagePath);
        ProductImage = imread(ProductImagePath);
        
        cd([curdir '\siftFeature']);
        tic
        [loc1, loc2, match, OutputImage] = CheckMatchingTwoImages(I, ProductImage);
        RunningTime = toc;
    
        nummatch = sum(match(:) > 0);
        if(nummatch > 4)
            Result = 1;
        else
            Result = 0;
        end
        
        %Convert to Rate 0 - 100
        n1 = 0;
        n2 = 4;
        n3 = 50;
        
        CheckedValue = nummatch;
        if(CheckedValue > n3)
            CheckedValue = n3;
        end
        if(CheckedValue > n2)
            PercentValue = 0.5 + (CheckedValue - n2) / (2 * (n3 - n2));
        else
            PercentValue = (CheckedValue - n1) / (2 * (n2 - n1));
        end
        ProductRate = PercentValue;
        Rate = ProductRate;
%         OutParam1 = OutputImage;
%         OutParam2 = loc1;
%         OutParam3 = match;
        
    catch ME 
        ErrorStatus = ['Error in check Product or Not:' ME.message];   
        Result = 0;
        Rate = 0;
        RunningTime = 0;
        
%         OutParam1 = 0;
%         OutParam2 = 0;
%         OutParam3 = 0;
    end
    
    cd(curdir);

end



% blue = [0 0 255];
% pixel = [10 10 200];
% ang_thres = 10; % degrees. You should change this to suit your needs
% ang = acosd(dot(blue/norm(blue),pixel/norm(pixel)));
% isBlue = ang <= ang_thres; % Apply angle threshold

