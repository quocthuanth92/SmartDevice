function [ Result, Rate, RunningTime, ErrorStatus  ] = CheckGoodDistanceImage( ImagePath )
%CHECKNEARORFAR Summary of this function goes here
%   Detailed explanation goes here
    ErrorStatus = ' ';
    
    warning('off','all');
    try
        I = imread(ImagePath);
        tic
        I1 = I;
        BW1 = im2bw(I1, graythresh(I1)); 
        BW1 = imfill(BW1, 'holes');
        se = strel('disk',10);        
        erodedBW = imerode(BW1,se);

        BW = erodedBW;
        v = sum(sum(BW(:,1:10) == 1)) + sum(sum(BW(:,end-10:end) == 1));
        V = sum(sum(BW(:,1:10) >= 0)) + sum(sum(BW(:,end-10:end) >= 0));
        PixRate = v/V;
        RunningTime = toc;
        
        if(PixRate < 0.4)
            isNotNearFar = 1;
            NotNearFarRate = 100;
        else 
            isNotNearFar = 0;
            NotNearFarRate = 0;
        end

        Result = isNotNearFar;
        Rate = NotNearFarRate;
%         OutParam1 = PixRate;
%         OutParam2 = BW; 
%         OutParam3 = 0;
        
    catch ME 
        ErrorStatus = ['Error in check NearFar or Not:' ME.message ' for ' ImagePath];   
        Result = 0;
        Rate = 0;
        RunningTime = 0;
        
%         OutParam1 = 0;
%         OutParam2 = I; 
%         OutParam3 = 0;
    end

end

