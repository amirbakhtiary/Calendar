//using AutoMapper;
//using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
////using Dto = Par.Infrastructures.Communication.Models;
//using ViewModel = Calendar.Endpoints.WebAPI.ViewModels;

//namespace Calendar.Endpoints.WebAPI.Extentions.Mapper
//{
//    public class DataProfile : Profile
//    {
//        public DataProfile()
//        {
//            CreateMap<ViewModel.AddEventItemModel, AddNewEventItemCommand>().ConstructUsing(au => new AddNewEventItemCommand
//            {
//                EventOrganizer = au.EventOrganizer,
//                EventTime = au.eve
//            });
//            CreateMap<ViewModel.StatementRequestModel, Dto.StatementRequestDto>().ConstructUsing(au => new Dto.StatementRequestDto
//            {
//                DecryptedCvv2 = Codec.DecryptStringAes(au.Cvv2, key),
//                DecryptedPin = Codec.DecryptStringAes(au.Pin, key),
//                CardExpirationYear = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(0, 2)),
//                CardExpirationMonth = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(3, 2))
//            });
//            CreateMap<ViewModel.TransferRequestModel, Dto.TransferRequestDto>().ConstructUsing(au => new Dto.TransferRequestDto
//            {
//                DecryptedCvv2 = Codec.DecryptStringAes(au.Cvv2, key),
//                DecryptedPin = Codec.DecryptStringAes(au.Pin, key),
//                CardExpirationYear = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(0, 2)),
//                CardExpirationMonth = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(3, 2))
//            });

//            CreateMap<ViewModel.BillRequestModel, Dto.BillRequestDto>().ConstructUsing(au => new Dto.BillRequestDto
//            {
//                //CardNumber = "",//(await mediator.Send(new GetCreditCardDetailQuery(au.CardNumber))).CardNumber,
//                DecryptedCvv2 = Codec.DecryptStringAes(au.Cvv2, key),
//                DecryptedPin = Codec.DecryptStringAes(au.Pin, key),
//                CardExpirationYear = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(0, 2)),
//                CardExpirationMonth = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(3, 2))
//            });

//            CreateMap<ViewModel.TopupRequestModel, Dto.TopupRequestDto>().ConstructUsing(au => new Dto.TopupRequestDto
//            {
//                DecryptedCvv2 = Codec.DecryptStringAes(au.Cvv2, key),
//                DecryptedPin = Codec.DecryptStringAes(au.Pin, key),
//                CardExpirationYear = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(0, 2)),
//                CardExpirationMonth = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(3, 2))
//            });

//            CreateMap<ViewModel.DynPinRequestModel, Dto.DynPinRequestDto>().ConstructUsing(au => new Dto.DynPinRequestDto
//            {
//                DecryptedCvv2 = Codec.DecryptStringAes(au.Cvv2, key),
//                DecryptedPin = null,
//                CardExpirationYear = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(0, 2)),
//                CardExpirationMonth = int.Parse(Codec.DecryptStringAes(au.CardExpiration, key).Substring(3, 2))
//            });

//            CreateMap<ViewModel.CardPasswordRequestModel, Dto.CardPasswordRequestDto>().ConstructUsing(au => new Dto.CardPasswordRequestDto
//            {
//                DecryptedPin = Codec.DecryptStringAes(au.Pin, key),
//            });
//        }
//    }
//}
