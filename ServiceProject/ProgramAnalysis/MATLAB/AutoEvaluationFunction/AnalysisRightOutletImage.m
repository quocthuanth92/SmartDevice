function [ Result, ConfidentValue ] = AnalysisRightOutletImage( CapturedImage, OutletImage )


%     Threshold = 0.75;
%     [Result, Reliability_Rate] = CheckingMatchingImg(CapturedImage, OutletImage);
%     ConfidentValue = Reliability_Rate;
    
    I = imread(CapturedImage);
    OutletImage = imread(OutletImage);
    
    curdir=cd;
    cd([curdir '\siftFeature']);
    [loc1, loc2, match, OutputImage] = CheckMatchingTwoImages(I, OutletImage);
    nummatch = sum(match(:) > 0);
    ConfidentValue = nummatch;
    
    if(nummatch > 150)
        Result = 1;
    else
        Result = 0;
    end
    
    cd(curdir);
    
end

